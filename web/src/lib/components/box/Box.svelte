<script lang="ts">
  import Breadcrumbs, { type BreadcrumbItem } from '$lib/components/breadcrumbs/Breadcrumbs.svelte';

  export let width: 'small' | 'big' = 'small';

  const getMaxWidth = () => {
    switch (width) {
      case 'big':
        return 'max-w-6xl';
      default:
        return 'max-w-2xl';
    }
  };

  export let breadcrumbItems: BreadcrumbItem[] | undefined = undefined;
</script>

<div class={`bg-gray-100 p-6 w-full ${getMaxWidth()} mx-auto rounded-lg shadow-md relative`}>
  {#if breadcrumbItems}
    <Breadcrumbs items={breadcrumbItems} />
  {/if}
  <div class="mt-2" />
  <slot />
</div>
