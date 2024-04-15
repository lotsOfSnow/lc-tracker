import createClient from 'openapi-fetch';
import type { paths } from '$lib/api/openapi';

const apiUrl = import.meta.env.VITE_PUBLIC_API_URL;

export const apiClient = createClient<paths>({ baseUrl: apiUrl });
