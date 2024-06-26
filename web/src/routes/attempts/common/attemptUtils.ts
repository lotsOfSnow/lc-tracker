import { z } from 'zod';
import { difficulties, type Difficulty } from '../difficulty';
import { apiClient, getApiOperation } from '$lib/api';
import { AppRoute } from '$lib/routes';
import type { BreadcrumbItem } from '$lib/components/breadcrumbs/Breadcrumbs.svelte';

export const baseAttemptSchema = z.object({
  problemId: z.string().uuid(),
  minutesSpent: z.coerce
    .number()
    .transform((val) => (val === 0 ? undefined : val)),
  date: z.string(),
  difficulty: z
    .custom<Difficulty>((val) => {
      return typeof val === 'string' || typeof val === 'number'
        ? val in difficulties
        : false;
    })
    .pipe(z.coerce.number())
    .transform((val) => val as Difficulty),
  hasUsedHelp: z.coerce.boolean().default(false),
  hasSolved: z.coerce.boolean().default(false),
  isRecap: z.coerce.boolean().default(false),
  note: z.string(),
});

export const loadProblems = async (f: typeof fetch) => {
  const operation = getApiOperation('/api/problems', 'get', 200);

  const response = await apiClient.GET(operation.path, { fetch: f });

  return response.data?.value
    ? {
        problems: response.data.value.map((x) => ({
          id: x.id,
          title: x.title,
        })),
      }
    : undefined;
};

export type ProblemFields = {
  id: string | undefined;
  title: string | null | undefined;
};

export const getAttemptBreadcrumbs = (current: string): BreadcrumbItem[] => [
  {
    label: 'Attempts',
    route: AppRoute.ATTEMPTS,
  },
  { label: current },
];
