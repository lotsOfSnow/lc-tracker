<script lang="ts">
  import FormErrors from '$lib/components/form/FormErrors.svelte';
  import ProblemFormCommon from '../common/ProblemFormCommon.svelte';
  import Button from '$lib/components/Button.svelte';
  import Box from '$lib/components/box/Box.svelte';
  import BoxHeader from '$lib/components/box/BoxHeader.svelte';
  import { getProblemBreadcrumbs } from '../common/problemUtils';

  export let form;
</script>

<Box breadcrumbItems={getProblemBreadcrumbs('New')}>
  <BoxHeader>Create</BoxHeader>

  <ProblemFormCommon>
    <Button class="mt-2" type="submit">Create</Button>
  </ProblemFormCommon>

  <FormErrors data={form} />
</Box>
