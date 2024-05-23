import { AppRoute } from '$lib/routes';
import type { BreadcrumbItem } from '$lib/components/breadcrumbs/Breadcrumbs.svelte';

export const getProblemBreadcrumbs = (current: string): BreadcrumbItem[] => [
  {
    label: 'Problems',
    route: AppRoute.PROBLEMS,
  },
  { label: current },
];
