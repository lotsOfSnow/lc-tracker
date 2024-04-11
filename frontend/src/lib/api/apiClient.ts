export const apiPostJson = async (f: typeof fetch, url: string, data?: unknown) => {
	const body = data ? JSON.stringify(data) : undefined;
	const options = getOptions('POST', body);

	const result = await f(url, options);

	if (result.ok) {
		return result;
	}

	console.error(result.status);
	
	throw new Error('API call failed');
};

const getOptions = (method: string, body?: string): RequestInit => {
	return {
		headers: {
			'Content-Type': 'application/json'
		},
		cache: 'no-cache',
		method,
		body
	};
};
