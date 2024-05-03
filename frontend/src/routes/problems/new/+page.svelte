<script lang="ts">
  import { enhance } from '$app/forms';
  import Input from '$lib/components/Input.svelte';
  import Label from '$lib/components/Label.svelte';
  import Button from '$lib/components/Button.svelte';
  import FormCloseButton from '$lib/components/FormCloseButton.svelte';
  import { AppRoute } from '$lib/routes';
  import FormErrors from '$lib/components/form/FormErrors.svelte';
  import type { apiSchemas } from '$lib/api';
  import { commonInputControlClassName } from '$lib/components/styleUtils';
  import { afterUpdate } from 'svelte';

  export let form;

  let methods: (apiSchemas['ProblemMethod'] & { id: number })[] = [];

  let shouldScroll = false;

  const addMethod = async () => {
    methods = [...methods, { id: methods.length + 1, name: 'aa' }];
    shouldScroll = true;
  };

  const removeMethod = (id: number) => {
    methods = methods.filter(method => method.id !== id);
  };

  const scrollToBottom = async (node: HTMLElement) => {
    node.scroll({ top: node.scrollHeight, behavior: 'smooth' });
  };

  afterUpdate(() => {
    if (shouldScroll) {
      scrollToBottom(container);
      shouldScroll = false;
    }
  });

  let container: HTMLDivElement;

</script>

<div class="bg-gray-100 p-6 w-full max-w-md mx-auto rounded-lg shadow-md relative">
  <h2 class="text-xl font-semibold text-gray-800">Create</h2>

  <FormCloseButton to={AppRoute.PROBLEMS} />
  <form method="POST" on:submit|preventDefault use:enhance>
    <div>
      <Label for="number">Number</Label>
      <Input required name="number" id="number" type="number" />
    </div>

    <div>
      <Label for="name">Name</Label>
      <Input required name="name" id="name" type="text" />
    </div>

    <div>
      <Label for="url">Url</Label>
      <Input required name="url" id="url" type="url" />
    </div>

    <Label class="text-2xl">Methods</Label>

    <div class="h-[500px] overflow-auto" bind:this={container}>

      {#each methods as method}
        <div class="mb-2 border border-gray-300 rounded-md p-2 relative">
          <button type="button"
                  class="absolute top-[-8px] right-1 text-4xl"
                  on:click={() => removeMethod(method.id)}>
            &times;
          </button>
          <div>
            <Label for={`name-${method.id}`}>Name</Label>
            <Input required name={`name-${method.id}`} id={`name-${method.id}`} value={method.name} type="text" />
          </div>
          <div>
            <Label for={`contents-${method.id}`}>Content</Label>
            <textarea id={`contents-${method.id}`} name={`contents-${method.id}`}
                      class={commonInputControlClassName}>{method.contents || ''}</textarea>
          </div>
        </div>
      {/each}

      <Button type="button" on:click={addMethod}>Add</Button>
    </div>

    <Button type="submit" class="mt-6">Create</Button>
  </form>

  <FormErrors data={form} />
</div>
