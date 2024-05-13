<script lang="ts">
  import type { ServerError, ValidationError } from '$lib/utils/actionsReturnTypes';

  export let errorData: {
    validationError?: ValidationError;
    serverError?: ServerError
  } | null | undefined;

  let errors: string[] = [];

  $: errorData, getErrors();

  const getErrors = () => {
    if (!errorData) {
      errors = [];
      return;
    }

    if (errorData.validationError) {
      errors = [...errors, ...errorData.validationError.formErrors];

      Object.values(errorData.validationError.fieldErrors).forEach(fieldErrorArray => {
        errors.push(...fieldErrorArray ?? []);
      });
    }

    if (errorData.serverError) {
      errors = [...errors, ...errorData.serverError.detail ?? []];
    }
  };
</script>

<div>
  <ul>
    {#each errors as error}
      <li class="mt-2 text-sm text-red-500">{error}</li>
    {/each}
  </ul>
</div>
