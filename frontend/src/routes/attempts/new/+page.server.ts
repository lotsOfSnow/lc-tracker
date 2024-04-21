import { type Actions, error, fail, redirect } from '@sveltejs/kit';
import { z } from 'zod';
import { AppRoute } from '$lib/routes';
import { apiClient, getApiOperation } from '$lib/api';
import { safeParseRequestFormData } from '$lib/utils/zodUtils';
import { difficulties } from '../difficulty';
import { baseSchema } from '../attemptZodSchema';

export const load = async () => {
  const operation = getApiOperation('/api/problems', 'get', 200);

  const response = await apiClient.GET(operation.path);

  return response.data?.value
    ? {
        problems: response.data.value.map((x) => ({
          id: x.id,
          name: x.name,
        })),
      }
    : error(400);
};

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

export type Method = (typeof difficulties)[number];

const schema = z.intersection(baseSchema, z.object({}));
