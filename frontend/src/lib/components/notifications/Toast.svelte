<script lang="ts">
  import { fade } from 'svelte/transition';
  import type { ToastType } from '$lib/components/notifications/toastType';
  import { createEventDispatcher } from 'svelte';

  export let type: ToastType = 'error';
  export let dismissible = true;

  const color = type === 'error' ? 'bg-red-500' : 'bg-green-500';

  const dispatch = createEventDispatcher();
</script>

<article class={`${color} text-white p-4 rounded-lg shadow-lg flex w-full max-w-xs`} transition:fade>
  <div class="flex-grow">
    <div class="font-bold">
      {#if type === 'success'}
        Success
      {:else}
        Error
      {/if}
    </div>

    <div>
      <slot />
    </div>
  </div>
  {#if dismissible}
    <button class="ml-4 text-lg font-semibold leading-none" on:click={() => dispatch('dismiss')}>&times;</button>
  {/if}


</article>
