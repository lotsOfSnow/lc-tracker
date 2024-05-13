<script lang="ts">
  import FormErrors from '$lib/components/form/FormErrors.svelte';
  import ProblemFormCommon from '../common/ProblemFormCommon.svelte';
  import Button from '$lib/components/Button.svelte';
  import Box from '$lib/components/box/Box.svelte';
  import BoxHeader from '$lib/components/box/BoxHeader.svelte';

  export let form;
</script>

<Box>
  <BoxHeader>Create</BoxHeader>

  <ProblemFormCommon>
    <Button type="submit" class="mt-2">Create</Button>
  </ProblemFormCommon>

  <FormErrors errorData={form} />
</Box>
