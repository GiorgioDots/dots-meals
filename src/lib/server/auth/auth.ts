import { AUTH_SECRET, AUTH_URL } from '$env/static/private';
import jwt from 'jsonwebtoken';
import type { User } from './types';
import axios from 'axios';
import type { JwtAuthRequestDTO, JwtAuthResponseDTO } from './auth-utils';
import type { Cookies } from '@sveltejs/kit';

export function validateToken(token: string) {
	try {
		const decoded = jwt.verify(token, AUTH_SECRET) as User;
		return decoded;
	} catch (error) {
		return null;
	}
}

export async function refreshToken(refrToken: string) {
	const body: JwtAuthRequestDTO = {
		grant_type: 'refresh_token',
		refresh_token: refrToken
	};
	try {
		const res = await axios.post<JwtAuthResponseDTO>(`${AUTH_URL}/oauth/token`, body);
		const user = validateToken(res.data.access_token);
		return {
			response: res.data,
			user
		};
	} catch {
		return null;
	}
}

export function setAuthCookies(cookies: Cookies, authData: JwtAuthResponseDTO) {
	cookies.set('auth_tkn', authData.access_token, {
		path: '/',
		httpOnly: true,
		expires: new Date(authData.expires_at)
	});
	cookies.set('auth_rfrsh', authData.refresh_token, {
		path: '/',
		httpOnly: true
	});
}

export function deleteAuthCookies(cookies: Cookies) {
	cookies.delete('auth_tkn', {
		path: '/'
	});
	cookies.delete('auth_rfrsh', {
		path: '/'
	});
}
