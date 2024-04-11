<script lang="ts">

	import { AppRoute } from '$lib/routes';

	interface Item {
		text: string,
		link: string,
	}

	const items: Item[] = [
		{
			text: 'Home',
			link: AppRoute.HOME
		},
		{
			text: 'Problems',
			link: AppRoute.PROBLEMS
		},
		{
			text: 'Attempts',
			link: AppRoute.ATTEMPTS
		}
	];
</script>
<nav>
	<ul>
		{#each items as item}
			<li><a href="{item.link}">{item.text}</a></li>
		{/each}
	</ul>
</nav>

<slot />
