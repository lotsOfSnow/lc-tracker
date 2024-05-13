import { type Actions, error, redirect } from '@sveltejs/kit';
import { z } from 'zod';
import { AppRoute } from '$lib/routes';
import { apiClient } from '$lib/api';
import { safeParseRequestFormData } from '$lib/utils/zodUtils';
import { difficulties } from '../difficulty';
import {
  baseAttemptSchema,
  loadProblems,
  type ProblemFields,
} from '../common/attemptUtils';
import { failServer, failValidation } from '$lib/utils/actionsReturnTypes';

export const load = async ({
  fetch,
}): Promise<{ problems: ProblemFields[] }> => {
  return (await loadProblems(fetch)) ?? error(400);
};

export const actions = {
  default: async (event) => {
    const parsingResult = await safeParseRequestFormData(event.request, schema);
    if (!parsingResult.success) {
      return failValidation(parsingResult);
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

    return failServer(result.error);
  },
} satisfies Actions;

export type Method = (typeof difficulties)[number];

const schema = z.intersection(baseAttemptSchema, z.object({}));
