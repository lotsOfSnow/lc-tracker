<script>
  import FormErrors from '$lib/components/form/FormErrors.svelte';
  import ProblemFormCommon from '../common/ProblemFormCommon.svelte';
  import Button from '$lib/components/Button.svelte';
  import Box from '$lib/components/box/Box.svelte';
  import BoxHeader from '$lib/components/box/BoxHeader.svelte';

  export let form;
  export let data;

</script>

<Box>
  <BoxHeader>{data.id}</BoxHeader>

  <ProblemFormCommon {data}>
    <input name="id" value={data.id} hidden>
    <Button type="submit" class="mt-2">Update</Button>
  </ProblemFormCommon>

  <FormErrors errorData={form} />
</Box>
