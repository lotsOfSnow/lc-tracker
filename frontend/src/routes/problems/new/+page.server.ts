import { type Actions, redirect } from '@sveltejs/kit';
import { AppRoute } from '$lib/routes';
import { apiClient, type apiSchemas } from '$lib/api';
import { safeParseRequestFormData } from '$lib/utils/zodUtils';
import { problemSchema } from '../common/problemSchema';
import { z } from 'zod';
import { failServer, failValidation } from '$lib/utils/actionsReturnTypes';

export const actions = {
  default: async (event) => {
    const parsingResult = await safeParseRequestFormData(event.request, schema);
    if (!parsingResult.success) {
      return failValidation(parsingResult);
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

    return failServer(result.error);
  },
} satisfies Actions;

const schema = z.intersection(
  problemSchema,
  z.object({
    slug: z.string().min(2),
  }),
);
