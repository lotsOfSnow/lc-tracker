<script>
  import { enhance } from '$app/forms';
  import Input from '$lib/components/Input.svelte';
  import Label from '$lib/components/Label.svelte';
  import Button from '$lib/components/Button.svelte';

  export let form;
</script>

<form method="POST" on:submit|preventDefault use:enhance>
  <div>
    <Label for="number">Number</Label>
    <Input id="number" name="number" type="number" />
  </div>

  <div>
    <Label for="name">Name</Label>
    <Input id="name" name="name" type="text" />
  </div>

  <div>
    <Label for="url">Url</Label>
    <Input id="url" name="url" type="text" />
  </div>

  <Button type="submit">Create</Button>
</form>

{#if form?.fieldErrors || form?.formErrors}
  <div>
    <ul>
      {#each ([...Object.entries(form.fieldErrors), ...Object.entries(form.formErrors)]).filter(x => x) as error}
        <li class="mt-2 text-sm text-red-500">{error}</li>
      {/each}
    </ul>
  </div>
{/if}
