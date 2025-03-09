import {
	deleteAuthCookies,
	refreshToken,
	setAuthCookies,
	validateToken
} from '$lib/server/auth/auth';
import { type Handle } from '@sveltejs/kit';

const handleAuth: Handle = async ({ event, resolve }) => {
	const token = event.cookies.get('auth_tkn');
	const refToken = event.cookies.get('auth_rfrsh');

	if (!token && !refToken) {
		event.locals.user = null;
		return resolve(event);
	}

	if (token) {
		event.locals.user = validateToken(token);
	}

	if (!event.locals.user) {
		if (refToken) {
			console.log('refreshing');
			const refResponse = await refreshToken(refToken);
			console.log('refreshed', refResponse);
			if (refResponse && refResponse.user) {
				setAuthCookies(event.cookies, refResponse.response);
				event.locals.user = refResponse.user;
			} else {
				deleteAuthCookies(event.cookies);
			}
		} else {
			event.locals.user = null;
		}
	}

	return resolve(event);
};

export const handle: Handle = handleAuth;
