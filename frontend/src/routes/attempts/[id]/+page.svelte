<script>
  import { enhance } from '$app/forms';
  import Button from '$lib/components/Button.svelte';
  import FormCloseButton from '$lib/components/FormCloseButton.svelte';
  import { AppRoute } from '$lib/routes';
  import AttemptFormCommonFields from '../common/AttemptFormCommonFields.svelte';

  export let form;
  export let data;

</script>

<div class="bg-gray-100 p-6 w-full max-w-md mx-auto rounded-lg shadow-md relative">
  <h2 class="text-xl font-semibold text-gray-800">{data.attempt.id}</h2>

  <FormCloseButton to={AppRoute.ATTEMPTS} />
  <form method="POST" on:submit|preventDefault use:enhance>
    <input name="id" value={data.attempt.id} hidden>
    <AttemptFormCommonFields problems={data.problems} src={data.attempt} />

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
