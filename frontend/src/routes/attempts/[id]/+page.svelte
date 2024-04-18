<script>
  import { enhance } from '$app/forms';
  import Input from '$lib/components/Input.svelte';
  import Label from '$lib/components/Label.svelte';
  import Button from '$lib/components/Button.svelte';
  import FormCloseButton from '$lib/components/FormCloseButton.svelte';
  import { AppRoute } from '$lib/routes';

  export let form;
  export let data;

</script>

<div class="bg-gray-100 p-6 w-full max-w-md mx-auto rounded-lg shadow-md relative">
  <h2 class="text-xl font-semibold text-gray-800">{data.id}</h2>

  <FormCloseButton to={AppRoute.ATTEMPTS} />
  <form method="POST" on:submit|preventDefault use:enhance>
    <input name="id" value={data.id} hidden>
    <div>
      <Label for="number">Problem</Label>
      <Input id="problemId" name="problemId" type="text" value={data.problemId} />
    </div>

    <div>
      <Label for="name">Minutes spent</Label>
      <Input id="minutesSpent" name="minutesSpent" type="number" value={data.minutesSpent} />
    </div>

    <Button type="submit" class="mt-2">Update</Button>
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
