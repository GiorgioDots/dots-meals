import { AUTH_URL } from '$env/static/private';
import type { JwtAuthRequestDTO, JwtAuthResponseDTO } from '$lib/server/auth/auth-utils';
import { error, redirect } from '@sveltejs/kit';
import axios from 'axios';
import type { PageServerLoad } from './$types';
import { setAuthCookies } from '$lib/server/auth/auth';

export const load: PageServerLoad = async ({ url, cookies }) => {
	const cbCode = url.searchParams.get('code');
	const cbState = url.searchParams.get('state');
	const cbStatus = url.searchParams.get('status');

	const state = cookies.get('auth_state');
	const verifier = cookies.get('auth_code_verifier');
	cookies.delete('auth_state', {
		path: '/auth/callback'
	});
	cookies.delete('auth_code_verifier', {
		path: '/auth/callback'
	});

	if (!state || !cbState || state != cbState) {
		return error(400, {
			message: 'Request forged'
		});
	}

	if (cbStatus) {
		switch (cbStatus) {
			case 'cancelled':
			default:
				return error(400, {
					message: 'Authorization was not accepted'
				});
		}
	}

	if (!cbCode || !verifier) {
		return error(400, {
			message: 'Request invalid'
		});
	}

	const body: JwtAuthRequestDTO = {
		code: cbCode,
		grant_type: 'token',
		code_verifier: verifier
	};
	try {
		const res = await axios.post<JwtAuthResponseDTO>(`${AUTH_URL}/oauth/token`, body);

		setAuthCookies(cookies, res.data);
	} catch (err: any) {
		if (axios.isAxiosError<AuthRequestError>(err) && err.response) {
			return error(400, {
				message: err.response.data.error
			});
		}
		return error(400, {
			message: 'Something went wrong'
		});
	}
	return redirect(302, '/welcome');
};

interface AuthRequestError {
	error: string;
}
