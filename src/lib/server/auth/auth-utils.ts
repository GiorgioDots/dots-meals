import crypto from 'crypto';

export function generateCodeVerifier() {
	const array = crypto.randomBytes(32);
	return Buffer.from(array)
		.toString('base64')
		.replace(/\+/g, '-')
		.replace(/\//g, '_')
		.replace(/=+$/, '');
}

export async function generateCodeChallenge(codeVerifier: string) {
	const digest = crypto.createHash('sha256').update(codeVerifier).digest();
	return Buffer.from(digest)
		.toString('base64')
		.replace(/\+/g, '-')
		.replace(/\//g, '_')
		.replace(/=+$/, '');
}

export function generateState() {
	const array = crypto.randomBytes(16);
	return Buffer.from(array)
		.toString('base64')
		.replace(/\+/g, '-')
		.replace(/\//g, '_')
		.replace(/=+$/, '');
}

export function getDotsAuthLoginUrl(
	authUrl: string,
	clientId: string,
	redirectUri: string,
	codeChallenge: string,
	state: string
) {
	return `${authUrl}/oauth/authorize?client_id=${clientId}&redirect_uri=${redirectUri}&code_challenge=${codeChallenge}&state=${state}`;
}

export interface JwtAuthRequestDTO {
	code?: string;
	code_verifier?: string;
	grant_type: GrantTypes;
	refresh_token?: string;
}

export type GrantTypes = 'token' | 'refresh_token';

export interface JwtAuthResponseDTO {
	access_token: string;
	refresh_token: string;
	expires_at: number;
}
