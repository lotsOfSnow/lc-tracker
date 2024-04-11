import { type Actions, fail } from '@sveltejs/kit';
import type { components } from '$lib/types/openapi';
import { z } from 'zod';
import { apiPostJson } from '$lib/api/apiClient';

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

		await apiPostJson(event.fetch, '/api/problems', request);
	}
} satisfies Actions;

const schema = z.object({
	name: z.string().min(3),
	number: z.coerce.number().min(1),
	url: z.string().url()
});
