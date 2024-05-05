<script lang="ts">
  import FormErrors from '$lib/components/form/FormErrors.svelte';
  import ProblemFormCommon from '../common/ProblemFormCommon.svelte';
  import Button from '$lib/components/Button.svelte';

  export let form;

</script>

<div class="bg-gray-100 p-6 w-full max-w-md mx-auto rounded-lg shadow-md relative">
  <h2 class="text-xl font-semibold text-gray-800">Create</h2>

  <ProblemFormCommon>
    <Button type="submit" class="mt-2">Create</Button>
  </ProblemFormCommon>

  <FormErrors data={form} />
</div>
