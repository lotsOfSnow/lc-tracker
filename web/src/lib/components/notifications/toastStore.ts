import { writable } from 'svelte/store';
import type { ToastType } from '$lib/components/notifications/toastType';

export const toasts = writable<ToastData[]>([]);

const defaultToastTimeout = 5000;

let n = Date.now();

export const addToast = (
  type: ToastType,
  message: string,
  dismissible: boolean = true,
  timeout: number | undefined = defaultToastTimeout,
) => {
  const id = (++n).toString(36);

  const data: ToastData = {
    id,
    type,
    dismissible,
    timeout,
    message,
  };

  toasts.update((all) => [{ ...data }, ...all]);

  if (data.timeout) {
    setTimeout(() => dismissToast(id), data.timeout);
  }
};

export const dismissToast = (id: string) => {
  toasts.update((all) => all.filter((value) => value.id !== id));
};

export type ToastData = {
  id: string;
  type: ToastType;
  dismissible: boolean;
  timeout?: number;
  message: string;
};
