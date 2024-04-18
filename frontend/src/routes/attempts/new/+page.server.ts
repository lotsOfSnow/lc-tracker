import { type Actions, fail, redirect } from '@sveltejs/kit';
import { z } from 'zod';
import { AppRoute } from '$lib/routes';
import { apiClient } from '$lib/api';
import { safeParseRequestFormData } from '$lib/utils/zodUtils';

export const actions = {
  default: async (event) => {
    const parsingResult = await safeParseRequestFormData(event.request, schema);
    if (!parsingResult.success) {
      return fail(400, parsingResult.error.flatten());
    }

    const result = await apiClient.POST('/api/attempts', {
      body: {
        ...parsingResult.data,
      },
      fetch: event.fetch,
    });

    if (result.data) {
      redirect(302, AppRoute.ATTEMPTS);
    }

    const error = result.error;

    return fail(400, { formErrors: [error.title], fieldErrors: {} });
  },
} satisfies Actions;

const schema = z.object({
  problemId: z.string().uuid(),
  minutesSpent: z.coerce.number().min(1),
});
