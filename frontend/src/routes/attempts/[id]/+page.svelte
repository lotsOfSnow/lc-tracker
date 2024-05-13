<script>
  import { enhance } from '$app/forms';
  import Button from '$lib/components/Button.svelte';
  import FormCloseButton from '$lib/components/FormCloseButton.svelte';
  import { AppRoute } from '$lib/routes';
  import AttemptFormCommonFields from '../common/AttemptFormCommonFields.svelte';
  import FormErrors from '$lib/components/form/FormErrors.svelte';
  import { addToast } from '$lib/components/notifications/toastStore';
  import Box from '$lib/components/box/Box.svelte';
  import BoxHeader from '$lib/components/box/BoxHeader.svelte';

  export let form;
  export let data;
</script>

<Box>
  <BoxHeader>{data.attempt.id}</BoxHeader>

  <FormCloseButton to={AppRoute.ATTEMPTS} />

  <form method="POST" on:submit|preventDefault use:enhance={() => {
    return async ({result,  update}) => {
      if (result.type === 'success') {
        addToast('success', 'Updated');
      }

      await update({reset: false})
    }
  }}>
    <input name="id" value={data.attempt.id} hidden>
    <AttemptFormCommonFields problems={data.problems} src={data.attempt} />

    <Button type="submit" class="mt-2">Update</Button>
  </form>

  <FormErrors errorData={form} />
</Box>
