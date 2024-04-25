<script>
  import { enhance } from '$app/forms';
  import Button from '$lib/components/Button.svelte';
  import FormCloseButton from '$lib/components/FormCloseButton.svelte';
  import { AppRoute } from '$lib/routes';
  import AttemptFormCommonFields from '../common/AttemptFormCommonFields.svelte';
  import FormErrors from '$lib/components/form/FormErrors.svelte';
  import { addToast } from '$lib/components/notifications/toastStore';

  export let form;
  export let data;
</script>

<div class="bg-gray-100 p-6 w-full max-w-md mx-auto rounded-lg shadow-md relative">
  <h2 class="text-xl font-semibold text-gray-800">{data.attempt.id}</h2>

  <FormCloseButton to={AppRoute.ATTEMPTS} />
  <form method="POST" on:submit|preventDefault use:enhance={() => {
    return async ({result,  update}) => {
      if (result.type === 'success') {
        addToast('success', false, 'Test', 1000);
      }

      await update({reset: false})
    }
  }}>
    <input name="id" value={data.attempt.id} hidden>
    <AttemptFormCommonFields problems={data.problems} src={data.attempt} />

    <Button type="submit" class="mt-2">Update</Button>
  </form>

  <FormErrors data={form} />
</div>
