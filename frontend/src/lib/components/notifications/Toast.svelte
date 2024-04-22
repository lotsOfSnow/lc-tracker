<script lang="ts">
  import { fade } from 'svelte/transition';
  import type { ToastType } from '$lib/components/notifications/toastType';
  import FormCloseButton from '$lib/components/FormCloseButton.svelte';
  import { createEventDispatcher } from 'svelte';

  export let type: ToastType = 'error';
  export let dismissible = true;

  const dispatch = createEventDispatcher();
</script>

<article class={type} role="alert" transition:fade>
  {#if type === 'success'}
    Success
  {:else}
    Error
  {/if}

  <div class="text">
    <slot />
  </div>

  {#if dismissible}
    <FormCloseButton on:click={() => dispatch('dismiss')} to="#" />
  {/if}


</article>
