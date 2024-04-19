import type { components } from '$lib/api/openapi';

export type Difficulty = components['schemas']['Difficulty'];

export const difficulties: Difficulty[] = [0, 1, 2, 3, 4];
