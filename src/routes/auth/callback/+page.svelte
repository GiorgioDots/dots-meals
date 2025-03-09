<!-- <script lang="ts">
	import { onMount } from 'svelte';
	import type { PageServerData } from './$types';
	import { PUBLIC_AUTH_URL } from '$env/static/public';
	import { type JwtAuthRequestDTO, type JwtAuthResponseDTO } from '$lib/client/utils/auth-utils';
	import axios from 'axios';

	let { data }: { data: PageServerData } = $props();
	let errorMessage = $state('Loading...');

	onMount(async () => {
		const cbState = data.state;
		const cbCode = data.code;
		const cbStatus = data.status;

		const state = sessionStorage.getItem('auth_state');
		const verifier = sessionStorage.getItem('auth_code_verifier');
		sessionStorage.removeItem('auth_state');
		sessionStorage.removeItem('auth_code_verifier');

		if (!state || !cbState || state != cbState) {
			errorMessage = 'Request forged';
			return;
		}

		if (cbStatus) {
			switch (cbStatus) {
				case 'cancelled':
				default:
					errorMessage = 'Authorization was not accepted';
					return;
			}
		}

		if (!cbCode || !verifier) {
			errorMessage = 'Request invalid';
			return;
		}

		const body: JwtAuthRequestDTO = {
			code: cbCode,
			grant_type: 'token',
			code_verifier: verifier
		};

		axios
			.post<JwtAuthResponseDTO>(`${PUBLIC_AUTH_URL}/oauth/token`, body)
			// fetch(`${PUBLIC_AUTH_URL}/oauth/token`, {
			// 	method: 'POST',
			// 	body: JSON.stringify(body)
			// })
			.then(async (res) => {
				// const data = (await res.json()) as JwtAuthResponseDTO;
				// Cookies.set('token', res.data.access_token);
			})
			.catch((err) => {
				errorMessage = err.error;
			});
	});
</script>

{errorMessage} -->
