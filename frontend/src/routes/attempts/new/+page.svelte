<script lang="ts">
  import Button from '$lib/components/Button.svelte';
  import FormCloseButton from '$lib/components/FormCloseButton.svelte';
  import { AppRoute } from '$lib/routes';
  import AttemptFormCommonFields from '../common/AttemptFormCommonFields.svelte';
  import FormErrors from '$lib/components/form/FormErrors.svelte';
  import Box from '$lib/components/box/Box.svelte';
  import BoxHeader from '$lib/components/box/BoxHeader.svelte';
  import Form from '$lib/components/form/Form.svelte';

  export let form;
  export let data;

  let problems = data.problems;
</script>

<Box>
  <BoxHeader>Create</BoxHeader>

  <FormCloseButton to={AppRoute.ATTEMPTS} />
  <Form>
    <AttemptFormCommonFields {problems} src={undefined} />

    <Button type="submit" class="mt-2">Create</Button>
  </Form>

  <FormErrors data={form} />
</Box>
