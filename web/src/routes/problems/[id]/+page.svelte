<script lang="ts">
  import FormErrors from '$lib/components/form/FormErrors.svelte';
  import ProblemFormCommon from '../common/ProblemFormCommon.svelte';
  import Button from '$lib/components/Button.svelte';
  import Box from '$lib/components/box/Box.svelte';
  import BoxHeader from '$lib/components/box/BoxHeader.svelte';
  import { getProblemBreadcrumbs } from '../common/problemUtils';

  export let form;
  export let data;

</script>

<Box breadcrumbItems={getProblemBreadcrumbs(data.id ?? 'Problem')}>
  <BoxHeader>Edit</BoxHeader>

  <ProblemFormCommon {data}>
    <input hidden name="id" value={data.id}>
    <Button class="mt-2" type="submit">Update</Button>
  </ProblemFormCommon>

  <FormErrors data={form} />
</Box>
