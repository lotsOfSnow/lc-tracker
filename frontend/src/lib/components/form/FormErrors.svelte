<script lang="ts">
  import type { ServerFailData, ValidationFailData } from '$lib/utils/actionsReturnTypes';

  type ErrorType = ServerFailData & ValidationFailData | undefined | null;

  export let errorData: ErrorType;

  let errors: string[] = [];

  $: errors = getErrors(errorData);

  const getErrors = (errorData: ErrorType): string[] => {
    console.log(errorData);
    
    if (!errorData) {
      return [];
    }

    const all: string[] = [];

    if (errorData.validationError) {
      all.push(...errorData.validationError.formErrors);

      Object.values(errorData.validationError.fieldErrors).forEach(fieldErrorArray => {
        all.push(...fieldErrorArray ?? []);
      });
    }

    if (errorData.serverError?.detail) {
      all.push(errorData.serverError.detail);
    }

    console.log(all);
    return all;

  };
</script>

<div>
  <ul>
    {#each errors as error}
      <li class="mt-2 text-sm text-red-500">{error}</li>
    {/each}
  </ul>
</div>
