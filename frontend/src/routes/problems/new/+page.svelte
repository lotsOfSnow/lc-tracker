<script>
	import { enhance } from '$app/forms';

	export let form;
</script>

<form method="POST" use:enhance on:submit|preventDefault>
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
		<input type="number" name="number" id="number" />
	</div>

	<div>
		<label for="name">Name</label>
		<input type="text" name="name" id="name" />
	</div>

	<div>
		<label for="url">Url</label>
		<input type="text" name="url" id="url" />
	</div>

	<button type="submit">Create</button>
</form>
