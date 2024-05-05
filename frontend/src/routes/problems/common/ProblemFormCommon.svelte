<script lang="ts">
  import { enhance } from '$app/forms';
  import Input from '$lib/components/Input.svelte';
  import Label from '$lib/components/Label.svelte';
  import Button from '$lib/components/Button.svelte';
  import type { apiSchemas } from '$lib/api';
  import { commonInputControlClassName } from '$lib/components/styleUtils';
  import { afterUpdate } from 'svelte';
  import FormCloseButton from '$lib/components/FormCloseButton.svelte';
  import { AppRoute } from '$lib/routes';

  export let data: apiSchemas['Problem'] | undefined = undefined;

  type Method = apiSchemas['ProblemMethod'];
  let methods: Method[] = data?.methods ?? [];

  let shouldScroll = false;

  const addMethod = () => {
    methods = [...methods, {}];
    shouldScroll = true;
  };

  const removeMethod = (index: number) => {
    methods = methods.filter(((_, i) => i !== index));
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

  const getMethodId = (index: number, type: keyof Method) => {
    return `methods[${index}][${type}]`;
  };

  let container: HTMLDivElement;

</script>

<FormCloseButton to={AppRoute.PROBLEMS} />

<form method="POST" on:submit|preventDefault
      use:enhance={async ({formData}) => formData.append(`methods`, JSON.stringify(methods))}
>
  <div>
    <Label for="number">Number</Label>
    <Input required name="number" id="number" type="number" value={data?.number} />
  </div>

  <div>
    <Label for="name">Name</Label>
    <Input required name="name" id="name" type="text" value={data?.name} />
  </div>

  <div>
    <Label for="url">Url</Label>
    <Input required name="url" id="url" type="url" value={data?.url} />
  </div>

  <Label class="text-2xl">Methods</Label>

  <div class="h-[500px] overflow-auto" bind:this={container}>

    {#each methods as method, i (i)}
      <div class="mb-2 border border-gray-300 rounded-md p-2 relative">
        <button type="button"
                class="absolute top-[-8px] right-1 text-4xl"
                on:click={() => removeMethod(i)}>
          &times;
        </button>
        <div>
          <Label for={getMethodId(i, 'name')}>Name</Label>
          <Input required id={getMethodId(i, 'name')} bind:value={method.name} type="text" />
        </div>
        <div>
          <Label for={getMethodId(i, 'contents')}>Content</Label>
          <textarea required id={getMethodId(i, 'contents')} bind:value={method.contents}
                    class={commonInputControlClassName} />
        </div>
      </div>
    {/each}

    <Button type="button" on:click={addMethod}>Add</Button>
  </div>

  <slot />
</form>
