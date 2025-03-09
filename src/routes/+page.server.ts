import type { Actions } from './$types';
import { AUTH_URL, AUTH_CLIENTID } from '$env/static/private';
import {
	generateCodeChallenge,
	generateCodeVerifier,
	generateState,
	getDotsAuthLoginUrl
} from '$lib/server/auth/auth-utils';
import { redirect } from '@sveltejs/kit';

export const actions: Actions = {
	async login({ cookies, url }) {
		const codeVerifier = generateCodeVerifier();
		const code_challenge = await generateCodeChallenge(codeVerifier);
		const state = generateState();
		cookies.set('auth_code_verifier', codeVerifier, {
			path: '/auth/callback',
			httpOnly: true,
			expires: new Date(Date.now() + 1000 * 60 * 5)
		});
		cookies.set('auth_state', state, {
			path: '/auth/callback',
			httpOnly: true,
			expires: new Date(Date.now() + 1000 * 60 * 5)
		});
		const gourl = getDotsAuthLoginUrl(
			AUTH_URL,
			AUTH_CLIENTID,
			`${url.origin}/auth/callback`,
			code_challenge,
			state
		);
		return redirect(303, gourl);
	}
};
