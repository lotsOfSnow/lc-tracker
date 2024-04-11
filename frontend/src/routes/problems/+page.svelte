<script lang="ts">

	import { AppRoute } from '$lib/routes';

	export let data;
</script>

<a href={AppRoute.PROBLEMS_NEW}>Go</a>
{#each data.problems as problem}
	<div>{problem.name}</div>
{/each}
