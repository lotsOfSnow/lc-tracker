<script lang="ts">
	import { routes } from '$lib/routes';

	interface Item {
		text: string,
		link: string,
	}

	const items: Item[] = [
		{
			text: 'Home',
			link: routes.home
		},
		{
			text: 'Problems',
			link: routes.problems
		},
		{
			text: 'Attempts',
			link: routes.attempts
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
