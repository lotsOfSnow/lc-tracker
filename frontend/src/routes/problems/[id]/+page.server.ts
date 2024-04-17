import { type Actions, fail, redirect } from '@sveltejs/kit';
import { AppRoute } from '$lib/routes';
import { z } from 'zod';
import { apiClient, getApiOperation } from '$lib/api';

export const actions = {
  default: async (event) => {
    const data = await event.request.formData();
    const entries = Object.fromEntries(data);
    console.log(data);
    const parsingResult = schema.safeParse(entries);
    if (!parsingResult.success) {
      return fail(400, parsingResult.error.flatten());
    }

    const parsed = parsingResult.data;

    const operation = getApiOperation('/api/problems/{id}', 'put', 204);

    const request = {
      name: parsed.name,
      number: parsed.number,
      url: parsed.url,
      id: parsed.id,
    };

    const result = await apiClient.PUT(operation.path, {
      body: request,
      fetch: event.fetch,
      params: {
        path: {
          id: parsed.id,
        },
      },
    });

    if (result.data) {
      // TODO: show toast instead
      redirect(302, AppRoute.PROBLEMS);
    }

    const error = result.error;

    return 'title' in error
      ? fail(400, { formErrors: [error.title], fieldErrors: {} })
      : fail(500);
  },
} satisfies Actions;

const schema = z.object({
  id: z.string(),
  name: z.string().min(3),
  number: z.coerce.number().min(1),
  url: z.string().url(),
});
