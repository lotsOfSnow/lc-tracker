import { type Actions, error, fail, redirect } from '@sveltejs/kit';
import { AppRoute } from '$lib/routes';
import { z } from 'zod';
import { apiClient, getApiOperation } from '$lib/api';
import { safeParseRequestFormData } from '$lib/utils/zodUtils';

export const actions = {
  default: async (event) => {
    const parsingResult = await safeParseRequestFormData(event.request, schema);
    if (!parsingResult.success) {
      return fail(400, parsingResult.error.flatten());
    }

    const result = await apiClient.PUT('/api/attempts/{id}', {
      body: {
        ...parsingResult.data,
      },
      fetch: event.fetch,
      params: {
        path: {
          id: parsingResult.data.id,
        },
      },
    });

    if (result.data) {
      redirect(302, AppRoute.ATTEMPTS);
    }

    const error = result.error;

    return 'title' in error
      ? fail(400, { formErrors: [error.title], fieldErrors: {} })
      : fail(500);
  },
} satisfies Actions;

export const load = async ({ params, fetch }) => {
  const operation = getApiOperation('/api/attempts/{id}', 'get', 200);

  const result = await apiClient.GET(operation.path, {
    fetch,
    params: {
      path: { id: params.id },
    },
  });

  return result.data ?? error(400);
};

const schema = z.object({
  id: z.string().uuid(),
  problemId: z.string().uuid(),
  minutesSpent: z.coerce.number().min(1),
});