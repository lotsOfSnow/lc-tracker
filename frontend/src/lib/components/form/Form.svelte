<script lang="ts">
  import { addToast } from '$lib/components/notifications/toastStore.js';
  import { enhance } from '$app/forms';

  export let successMessage: string = 'Saved';

  export let beforeSubmit: ((formData: FormData) => unknown) | undefined = undefined;
</script>
<form method="POST" on:submit|preventDefault use:enhance={({formData}) => {
  if (beforeSubmit){
    beforeSubmit(formData);
  }
    return async ({result,  update}) => {
      if (result.type === 'success' || result.type === 'redirect') {
        addToast('success', successMessage);
      }
      if (result.type === 'error' || result.type === 'failure') {
        addToast('error', "Failed");
      }

      await update({reset: false})
    }
  }}>
  <slot />
</form>
