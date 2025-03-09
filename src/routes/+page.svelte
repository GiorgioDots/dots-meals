<script lang="ts">
	import { enhance } from '$app/forms';

	// import { PUBLIC_AUTH_URL, PUBLIC_AUTH_CLIENTID } from '$env/static/public';
	// import {
	// 	generateCodeChallenge,
	// 	generateCodeVerifier,
	// 	generateState,
	// 	getDotsAuthLoginUrl
	// } from '$lib/client/utils/auth-utils';

	const gotoLogin = async () => {
		// const codeVerifier = generateCodeVerifier();
		// const code_challenge = await generateCodeChallenge(codeVerifier);
		// const state = generateState();
		// sessionStorage.setItem('auth_code_verifier', codeVerifier);
		// sessionStorage.setItem('auth_state', state);
		// const url = getDotsAuthLoginUrl(
		// 	PUBLIC_AUTH_URL,
		// 	PUBLIC_AUTH_CLIENTID,
		// 	`${location.origin}/auth/callback`,
		// 	code_challenge,
		// 	state
		// );
		// window.location.href = url;
	};
</script>

<form action="?/login" use:enhance method="POST">
	<button class="btn btn-primary">Login</button>
</form>
