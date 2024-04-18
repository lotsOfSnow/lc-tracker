<script>
  import { enhance } from '$app/forms';
  import Input from '$lib/components/Input.svelte';
  import Label from '$lib/components/Label.svelte';
  import Button from '$lib/components/Button.svelte';
  import FormCloseButton from '$lib/components/FormCloseButton.svelte';
  import { AppRoute } from '$lib/routes';

  export let form;
</script>

<div class="bg-gray-100 p-6 w-full max-w-md mx-auto rounded-lg shadow-md relative">
  <h2 class="text-xl font-semibold text-gray-800">Create</h2>

  <FormCloseButton to={AppRoute.ATTEMPTS} />
  <form method="POST" on:submit|preventDefault use:enhance>
    <div>
      <Label for="number">Problem ID</Label>
      <Input id="problemId" name="problemId" type="string" />
    </div>

    <div>
      <Label for="name">Minutes spent</Label>
      <Input id="minutesSpent" name="minutesSpent" type="number" />
    </div>

    <Button type="submit" class="mt-2">Create</Button>
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
</div>
