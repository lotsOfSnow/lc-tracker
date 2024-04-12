import { type Actions, fail, redirect } from '@sveltejs/kit';
import type { components } from '$lib/types/openapi';
import { z } from 'zod';
import { apiPostJson } from '$lib/api/apiClient';
import { AppRoute } from '$lib/routes';

export const actions = {
	default: async (event) => {
		const data = await event.request.formData();
		const entries = Object.fromEntries(data);

		const parsingResult = schema.safeParse(entries);
		if (!parsingResult.success) {
			console.error(parsingResult.error.flatten());
			return fail(400, parsingResult.error.flatten());
		}

		const parsed = parsingResult.data;

		const request: components['schemas']['CreateProblemRequest'] = {
			name: parsed.name,
			number: parsed.number,
			url: parsed.url
		};

		const result = await apiPostJson(event.fetch, '/api/problems', request);

		if (result.value) {
			redirect(302, AppRoute.PROBLEMS);
		}

		const title = result.error?.title;

		return fail(400, { formErrors: [title], fieldErrors: {} });
	}
} satisfies Actions;

const schema = z.object({
	name: z.string().min(3),
	number: z.coerce.number().min(1),
	url: z.string().url()
});
