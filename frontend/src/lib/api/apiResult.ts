import type { ApiProblemDetails } from '$lib/api/apiProblemDetails';

export interface ApiResult {
	error?: ApiProblemDetails;
	value?: Response;
}
