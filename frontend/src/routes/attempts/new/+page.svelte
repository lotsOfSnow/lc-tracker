<script lang="ts">
  import { enhance } from '$app/forms';
  import Button from '$lib/components/Button.svelte';
  import FormCloseButton from '$lib/components/FormCloseButton.svelte';
  import { AppRoute } from '$lib/routes';
  import AttemptFormCommonFields from '../common/AttemptFormCommonFields.svelte';
  import FormErrors from '$lib/components/form/FormErrors.svelte';
  import Box from '$lib/components/box/Box.svelte';
  import BoxHeader from '$lib/components/box/BoxHeader.svelte';

  export let form;
  export let data;

  let problems = data.problems;
</script>

<Box>
  <BoxHeader>Create</BoxHeader>

  <FormCloseButton to={AppRoute.ATTEMPTS} />
  <form method="POST" on:submit|preventDefault use:enhance>
    <AttemptFormCommonFields {problems} src={undefined} />

    <Button type="submit" class="mt-2">Create</Button>
  </form>

  <FormErrors data={form} />
</Box>
