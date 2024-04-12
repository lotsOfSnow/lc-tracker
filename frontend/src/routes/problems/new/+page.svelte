<script>
	import { enhance } from '$app/forms';

	export let form;
</script>

<form method="POST" on:submit|preventDefault use:enhance>
	{#if form?.fieldErrors || form?.formErrors}
		<div>
			<ul>
				{#each ([...Object.entries(form.fieldErrors), ...Object.entries(form.formErrors)]).filter(x => x) as error}
					<li>{error}</li>
				{/each}
			</ul>
		</div>
	{/if}
	<div>
		<label for="number">Number</label>
		<input id="number" name="number" type="number" />
	</div>

	<div>
		<label for="name">Name</label>
		<input id="name" name="name" type="text" />
	</div>

	<div>
		<label for="url">Url</label>
		<input id="url" name="url" type="text" />
	</div>

	<button type="submit">Create</button>
</form>
