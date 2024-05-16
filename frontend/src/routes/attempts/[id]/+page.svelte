<script>
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
</script>

<Box>
  <BoxHeader>{data.attempt.id}</BoxHeader>

  <FormCloseButton to={AppRoute.ATTEMPTS} />

  <Form>
    <input name="id" value={data.attempt.id} hidden>
    <AttemptFormCommonFields problems={data.problems} src={data.attempt} />

    <Button type="submit" class="mt-2">Update</Button>
  </Form>

  <FormErrors data={form} />
</Box>
