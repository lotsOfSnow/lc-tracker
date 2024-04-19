import type { components } from '$lib/api/openapi';

type Difficulty = components['schemas']['Difficulty'];

export const difficulties: Array<Difficulty> = [0, 1, 2, 3, 4];
