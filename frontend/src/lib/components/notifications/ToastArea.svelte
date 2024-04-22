<script>
  import Toast from '$lib/components/notifications/Toast.svelte';
  import { dismissToast, toasts } from '$lib/components/notifications/toastStore.js';
</script>

{#if $toasts}
  <section class="fixed top-0 left-0 right-0 w-full z-[1000]">
    {#each $toasts as toast (toast.id)}
      <Toast
        type={toast.type}
        dismissible={toast.dismissible}
        on:dismiss={() => dismissToast(toast)}>{toast.message}</Toast>
    {/each}
  </section>
{/if}
