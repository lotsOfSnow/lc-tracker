<script>
  import { enhance } from '$app/forms';
  import Input from '$lib/components/Input.svelte';
  import Label from '$lib/components/Label.svelte';
  import Button from '$lib/components/Button.svelte';
  import FormCloseButton from '$lib/components/FormCloseButton.svelte';
  import { AppRoute } from '$lib/routes';
  import FormErrors from '$lib/components/form/FormErrors.svelte';

  export let form;
  export let data;

</script>

<div class="bg-gray-100 p-6 w-full max-w-md mx-auto rounded-lg shadow-md relative">
  <h2 class="text-xl font-semibold text-gray-800">{data.id}</h2>

  <FormCloseButton to={AppRoute.PROBLEMS} />
  <form method="POST" on:submit|preventDefault use:enhance>
    <input name="id" value={data.id} hidden>
    <div>
      <Label for="number">Number</Label>
      <Input name="number" type="number" value={data.number} />
    </div>

    <div>
      <Label for="name">Name</Label>
      <Input name="name" type="text" value={data.name} />
    </div>

    <div>
      <Label for="url">Url</Label>
      <Input name="url" type="url" value={data.url} />
    </div>

    <Button type="submit" class="mt-2">Update</Button>
  </form>

  <FormErrors data={form} />
</div>
