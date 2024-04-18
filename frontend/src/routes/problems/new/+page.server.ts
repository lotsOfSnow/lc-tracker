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

    const parsed = parsingResult.data;

    const request: apiSchemas['CreateProblemRequest'] = {
      name: parsed.name,
      number: parsed.number,
      url: parsed.url,
    };

    const result = await apiClient.POST('/api/problems', {
      body: request,
      fetch: event.fetch,
    });

    if (result.data) {
      redirect(302, AppRoute.PROBLEMS);
    }

    const error = result.error;

    return fail(400, { formErrors: [error.title], fieldErrors: {} });
  },
} satisfies Actions;

const schema = z.object({
  name: z.string().min(3),
  number: z.coerce.number().min(1),
  url: z.string().url(),
});
