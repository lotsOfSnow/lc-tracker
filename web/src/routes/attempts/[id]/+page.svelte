<script>
  import Button from '$lib/components/Button.svelte';
  import AttemptFormCommonFields from '../common/AttemptFormCommonFields.svelte';
  import FormErrors from '$lib/components/form/FormErrors.svelte';
  import Box from '$lib/components/box/Box.svelte';
  import BoxHeader from '$lib/components/box/BoxHeader.svelte';
  import Form from '$lib/components/form/Form.svelte';
  import { getAttemptBreadcrumbs } from '../common/attemptUtils';

  export let form;
  export let data;
</script>

<Box breadcrumbItems={getAttemptBreadcrumbs(data.attempt.id ?? 'Attempt')}>
  <BoxHeader>Edit</BoxHeader>

  <Form>
    <input hidden name="id" value={data.attempt.id}>
    <AttemptFormCommonFields problems={data.problems} src={data.attempt} />

    <Button class="mt-2" type="submit">Update</Button>
  </Form>

  <FormErrors data={form} />
</Box>
