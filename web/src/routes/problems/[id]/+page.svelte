<script lang="ts">
  import FormErrors from '$lib/components/form/FormErrors.svelte';
  import ProblemFormCommon from '../common/ProblemFormCommon.svelte';
  import Button from '$lib/components/Button.svelte';
  import Box from '$lib/components/box/Box.svelte';
  import BoxHeader from '$lib/components/box/BoxHeader.svelte';
  import { type BreadcrumbItem } from '$lib/components/breadcrumbs/Breadcrumbs.svelte';
  import { AppRoute } from '$lib/routes';

  export let form;
  export let data;

  const items: BreadcrumbItem[] = [
    { label: 'Problems', route: AppRoute.PROBLEMS },
    { label: data.id ?? 'Problem' },
  ];

</script>

<Box breadcrumbItems={items}>
  <BoxHeader>{data.id}</BoxHeader>

  <ProblemFormCommon {data}>
    <input hidden name="id" value={data.id}>
    <Button class="mt-2" type="submit">Update</Button>
  </ProblemFormCommon>

  <FormErrors data={form} />
</Box>
