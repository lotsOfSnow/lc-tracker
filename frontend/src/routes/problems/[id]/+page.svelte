<script>
  import FormErrors from '$lib/components/form/FormErrors.svelte';
  import ProblemFormCommon from '../common/ProblemFormCommon.svelte';
  import Button from '$lib/components/Button.svelte';

  export let form;
  export let data;

</script>

<div class="bg-gray-100 p-6 w-full max-w-md mx-auto rounded-lg shadow-md relative">
  <h2 class="text-xl font-semibold text-gray-800">{data.id}</h2>

  <ProblemFormCommon {data}>
    <Button type="submit" class="mt-2">Update</Button>
  </ProblemFormCommon>

  <FormErrors data={form} />
</div>
