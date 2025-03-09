import { redirect } from '@sveltejs/kit';
import type { PageServerLoad } from './$types';

export const load: PageServerLoad = async (event) => {
	console.log(event.locals.user);
	if (!event.locals.user) return redirect(302, '/');
	return {};
};
