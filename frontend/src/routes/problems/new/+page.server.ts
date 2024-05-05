import { type Actions, fail, redirect } from '@sveltejs/kit';
import { z } from 'zod';
import { AppRoute } from '$lib/routes';
import { apiClient, type apiSchemas } from '$lib/api';
import { safeParseRequestFormData } from '$lib/utils/zodUtils';

export const actions = {
  default: async (event) => {
    const parsingResult = await safeParseRequestFormData(event.request, schema);
    if (!parsingResult.success) {
      return fail(400, parsingResult.error.flatten());
    }

    const request: apiSchemas['CreateProblemRequest'] = {
      ...parsingResult.data,
    };

    const result = await apiClient.POST('/api/problems', {
      body: request,
      fetch: event.fetch,
    });

    if (result.data) {
      redirect(302, AppRoute.PROBLEMS);
    }

    const error = result.error;

    return fail(400, { serverErrors: [error.title] });
  },
} satisfies Actions;

const methodSchema = z.object({
  name: z.string().min(1),
  contents: z.string().min(1),
});

const schema = z.object({
  name: z.string().min(3),
  number: z.coerce.number().min(1),
  url: z.string().url(),
  methods: z.string().transform((json) => {
    const parsed = JSON.parse(json);
    return z.array(methodSchema).parse(parsed);
  }),
});
