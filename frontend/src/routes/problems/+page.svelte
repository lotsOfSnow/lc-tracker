<script lang="ts">

	import { AppRoute } from '$lib/routes';

	export let data;
</script>

<a href={AppRoute.PROBLEMS_NEW}>Create new</a>
{#each data.problems as problem}
	<div>{problem.number}</div>
	<div>{problem.name}</div>
	<div>{problem.url}</div>

{/each}
